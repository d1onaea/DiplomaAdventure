// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

//	============================================================
//	Name:		AtlasShader
//	Author: 	Joen Joensen (@UnLogick)
//	============================================================

Shader "UMA/Atlas/AtlasShaderNormal_SoftLight" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_AdditiveColor ("Additive Color", Color) = (0,0,0,0)
	_MainTex ("Normalmap", 2D) = "bump" {}
	_ExtraTex ("mask", 2D) = "white" {}
	_BaseTex ("Blendbase Texture", 2D) = "white" {}
}

SubShader 
{
	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}

	Pass
	{
		Tags{ "LightMode" = "Vertex" }
		Fog{ Mode Off }
		Blend SrcAlpha OneMinusSrcAlpha

		Lighting Off
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag

		#include "UnityCG.cginc"
		#include "UnityStandardUtils.cginc"

		float4 _Color;
		float4 _AdditiveColor;
		sampler2D _MainTex;
		sampler2D _ExtraTex;
		sampler2D _BaseTex;

		struct v2f {
			float4  pos : SV_POSITION;
			float2  uv : TEXCOORD0;
		};

		float4 _MainTex_ST;

		v2f vert(appdata_base v)
		{
			v2f o;
			o.pos = UnityObjectToClipPos(v.vertex);
			o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
			return o;
		}

float BlendMode_SoftLight(float base, float blend)
{
	if (blend <= 0.5)
	{
		return base - (1-2*blend)*base*(1-base);
	}
	else
	{
		float d = (base <= 0.25) ? ((16*base-12)*base+4)*base : sqrt(base);
		return base + (2*blend-1)*(d-base);
	}
}

float3 BlendMode_SoftLight(float3 base, float3 blend)
{
	return float3(  BlendMode_SoftLight(base.r, blend.r), 
					BlendMode_SoftLight(base.g, blend.g), 
					BlendMode_SoftLight(base.b, blend.b) );
}

		half4 frag(v2f i) : COLOR
		{
			half4 n = tex2D(_MainTex, i.uv);
			half4 extra = tex2D(_ExtraTex, i.uv);
			float4 basecol = tex2D(_BaseTex, i.uv);

#if !defined(UNITY_NO_DXT5nm)
			//swizzle the alpha and red channel, we will swizzle back in the post process SwizzleShader
			n.r = n.a;
#endif
			n.rgb = BlendMode_SoftLight(basecol.rgb, n.rgb); // subtract the overlay from the previous pass
			n.a = min(extra.a, _Color.a);
			return n * _Color + _AdditiveColor;
		}
		ENDCG
	}
}

Fallback "Transparent/VertexLit"
} 
