// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "MyShaders/DistortionEffectShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_NoiseTex("Noise Texture", 2D) = "white" {}
		_NoiseMagnitude("Noise Magnitude", Range(0, 1)) = 0
		_Color("Tint", Color) = (1, 1, 1, 1)
	}
		SubShader
	{
		Tags
	{
		"Queue" = "Transparent"
	}


		Pass
	{

		Blend SrcAlpha OneMinusSrcAlpha

		CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"


		sampler2D _MainTex;
		sampler2D _NoiseTex;
		float _NoiseMagnitude;
	half4 _Color;
	float4 _MainTex_ST;

	struct appdata
	{
		float4 vertex : POSITION;
		float2 uv : TEXCOORD0;
	};

	struct v2f
	{
		float4 vertex : SV_POSITION;
		float2 uv : TEXCOORD0;
	};

	v2f vert(appdata v)
	{
		v2f OUT;

		OUT.vertex = UnityObjectToClipPos(v.vertex);
		OUT.uv = v.uv;

		return OUT;
	}

	fixed4 frag(v2f i) : SV_Target
	{
		//The NoiseTex will be the shape of the pattern of 'time & space distort' effect this is freaking cool!!!
		float2 offset;

		offset = tex2D(_NoiseTex, i.uv).xy;
		offset = (offset * 2) - 1; //if original offset is between 0 ~ 1 this will turn it into -1 ~ 1
		
		float mag = _NoiseMagnitude * sin(_Time.y);

		offset = offset * mag;

		float4 col = tex2D(_MainTex, i.uv + offset);

		return col;
	}
		ENDCG
	}
	}
}
