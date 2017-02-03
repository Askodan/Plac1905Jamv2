Shader "Unlit/KinolowyShadert2"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Intensity("Intensity", Range(0.0, 1.0)) = 0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _Intensity;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed2 uv = (i.uv - 0.5) * 2.0;
				fixed4 col = _Intensity * fixed4(frac(i.uv.x * 10.0) + sin(i.uv.x + _Time.y * 3.0), sin((uv.x * uv.x + uv.y * uv.y) * 15.0 + _Time.y * 7.0), 0.0, 1.0);
				
				col += tex2D(_MainTex, i.uv);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
