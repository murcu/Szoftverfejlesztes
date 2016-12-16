Shader "Tut/MultiColor" {
	Properties{
		_MainTex("Texture", 2D) = "white"{}
	}

	SubShader{
		Pass{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f{
				float4 vertex : SV_POSITION;
				float2 uv : TEXTCOORD0;
			};

			v2f vert(appdata v){
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;

			float4 frag(v2f i ) : SV_Target{
				float4 color;
				color = tex2D(_MainTex, i.uv);
				color *= float4(i.uv.x, i.uv.y, 0, 1);
				//color = float4(i.uv.x, i.uv.y, 1, 1);
				return color;
			}
			ENDCG
		}
	}
}
