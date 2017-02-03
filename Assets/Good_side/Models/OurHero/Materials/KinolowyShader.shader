Shader "Custom/KinolowyShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Intensity("Intensity", Range(0.0, 1.0)) = 0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		float _Intensity;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed2 uv = (IN.uv_MainTex - 0.5) * 2.0;
			fixed4 col = _Intensity * fixed4(sin(IN.uv_MainTex.x + _Time.y * 3.0), sin((uv.x * uv.x + uv.y * uv.y) * 15.0 + _Time.y * 7.0), 0.0, 1.0);

			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic * length(col);
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
