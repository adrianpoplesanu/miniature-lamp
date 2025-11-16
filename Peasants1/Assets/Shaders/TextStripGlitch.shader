Shader "UI/TextStripGlitch"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}

        _GlitchStrength ("Glitch Strength", Range(0, 1)) = 0
        _StripDensity ("Strip Density", Range(1, 200)) = 60
        _StripWidth ("Strip Width", Range(0.01, 0.5)) = 0.08
        _MaxOffset ("Max X Offset", Range(-50, 50)) = 8

        _NoiseSpeed ("Noise Scroll Speed", Range(0, 10)) = 2
        _NoiseScale ("Noise Scale", Range(0.1, 10)) = 3
		
		// Provided by script in local (object) space
		_RectLocalXMin ("Rect Local X Min", Float) = 0
		_RectLocalWidth ("Rect Local Width", Float) = 100
		_RectLocalYMin ("Rect Local Y Min", Float) = 0
		_RectLocalHeight ("Rect Local Height", Float) = 50
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            Name "Default"
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            #pragma multi_compile_local _ UNITY_UI_CLIP_RECT
            #pragma multi_compile_local _ UNITY_UI_ALPHACLIP

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
                float4 worldPosition : TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _ClipRect;
            float4 _MainTex_TexelSize;

            float _GlitchStrength;
            float _StripDensity;
            float _StripWidth;
            float _MaxOffset;
            float _NoiseSpeed;
            float _NoiseScale;
			
			// Local-space rect info set from script (RectTransform rect)
			float _RectLocalXMin;
			float _RectLocalWidth;
			float _RectLocalYMin;
			float _RectLocalHeight;

            // 2D hash (returns 0-1)
            float hash21(float2 p)
            {
                p = frac(p * float2(123.34, 345.45));
                p += dot(p, p + 34.345);
                return frac(p.x * p.y);
            }

            v2f vert(appdata_t v)
            {
                v2f OUT;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
                
				// Calculate normalized local Y within this text rect [0..1]
				float localY01 = 0.0;
				if (_RectLocalHeight > 0.0)
				{
					localY01 = saturate((v.vertex.y - _RectLocalYMin) / _RectLocalHeight);
				}

				// Split into two halves (top / bottom chunks)
				float upperMask = step(0.5, localY01); // 1 for upper half, 0 for lower
				float lowerMask = 1.0 - upperMask;

				// Time-based random offset per half
				float timeFactor = _Time.y * _NoiseSpeed;
				float tstep = floor(timeFactor * _NoiseScale);
				float upperRand = hash21(float2(7.13, tstep)); // deterministic per frame
				float lowerRand = hash21(float2(3.71, tstep));

				// Convert 0..1 to -1..1
				float upperJitter = upperRand * 2.0 - 1.0;
				float lowerJitter = lowerRand * 2.0 - 1.0;

				// Optional pulsing gate
				float activePulse = saturate(sin(timeFactor * 6.28318) * 0.5 + 0.5);
				float gate = activePulse * _GlitchStrength;

				float upperOffset = upperJitter * _MaxOffset * gate;
				float lowerOffset = lowerJitter * _MaxOffset * gate;
				float offset = upperOffset * upperMask + lowerOffset * lowerMask;
				v.vertex.x += offset;

                OUT.worldPosition = v.vertex;
                OUT.vertex = UnityObjectToClipPos(v.vertex);
				OUT.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                OUT.color = v.color;

                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
				// Legacy UI Text: use vertex color for RGB, font texture alpha for A
				fixed4 tex = tex2D(_MainTex, IN.texcoord);
				half4 color = half4(IN.color.rgb, tex.a * IN.color.a);

                #ifdef UNITY_UI_CLIP_RECT
                color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
                #endif

                #ifdef UNITY_UI_ALPHACLIP
                clip (color.a - 0.001);
                #endif

                return color;
            }
            ENDCG
        }
    }

    FallBack "UI/Default"
}
