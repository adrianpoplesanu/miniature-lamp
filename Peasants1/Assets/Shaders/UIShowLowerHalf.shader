Shader "UI/UIShowLowerHalf"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}

		// Provided by script in local (object) space
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
				float4 vertex        : SV_POSITION;
				fixed4 color         : COLOR;
				float2 texcoord      : TEXCOORD0;
				float4 worldPosition : TEXCOORD1;
				float  localY01      : TEXCOORD2;
				UNITY_VERTEX_OUTPUT_STEREO
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _ClipRect;

			float _RectLocalYMin;
			float _RectLocalHeight;

			v2f vert(appdata_t v)
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				// Pass local Y delta (vertexY - centerY). Negative/zero => lower half, positive => upper.
				float centerLocalY = _RectLocalYMin + (_RectLocalHeight * 0.5);
				float localYDelta = v.vertex.y - centerLocalY;

				o.worldPosition = v.vertex;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.color = v.color;
				o.localY01 = localYDelta;
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				// Base UI text color: vertex RGB, font texture alpha for A
				fixed4 tex = tex2D(_MainTex, i.texcoord);
				half4 color = half4(i.color.rgb, tex.a * i.color.a);

				// Hard discard: keep lower half (localYDelta <= 0), discard upper (localYDelta > 0)
				clip(-i.localY01 + 1e-6);

				#ifdef UNITY_UI_CLIP_RECT
				color.a *= UnityGet2DClipping(i.worldPosition.xy, _ClipRect);
				#endif

				#ifdef UNITY_UI_ALPHACLIP
				clip(color.a - 0.001);
				#endif

				return color;
			}
			ENDCG
		}
	}

	FallBack "UI/Default"
}

