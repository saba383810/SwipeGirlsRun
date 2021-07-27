// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/VertexColorShader" {
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
	    _Color("_Color", COLOR) = (1,1,1,1)
	}

	  SubShader{
		Tags{ "RenderType" = "Opaque" }

		LOD 100

		Lighting Off

		Pass{
		  Blend SrcAlpha OneMinusSrcAlpha
		  Cull Back

		  CGPROGRAM
          #pragma vertex vert
          #pragma fragment frag
          #include "UnityCG.cginc"

		  sampler2D _MainTex;
	      float4 _MainTex_ST;
	      //fixed _Cutoff;
		  float4 _Color;

		  struct appdata_t {
		    float4 pos : POSITION;
		    float2 texcoord : TEXCOORD0;
		    float4 color : COLOR;
	      };

	      struct v2f {
	        //half2 texcoord : TEXCOORD0;
		    float2 texcoord	: TEXCOORD0;
		    float4 pos : SV_POSITION;
		    float4 color : COLOR;
	      };

		  // vertex
	      v2f vert(appdata_full v) {
		    v2f o;
		    o.pos = UnityObjectToClipPos(v.vertex);
		    //o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
		    o.texcoord = v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
		    o.color = v.color;
		    return o;
	      }

	      // fragment
	      float4 frag(v2f i) : SV_Target{
		    float4 col = tex2D(_MainTex, i.texcoord);
	        //clip(col.a - _Cutoff);
	        col *= i.color;
		    return col;// *i.color;
	      }
		  ENDCG
	    }
	 }

}