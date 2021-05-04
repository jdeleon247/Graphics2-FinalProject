Shader "Custom/BaseShader"
{
	Properties 
	{
		_MainTexture("Texture", 2D) = "white" {}
		_Color("Color", Color) = (1, 1, 1, 1)
	}

		SubShader
		{

			Pass
			{
				CGPROGRAM

				#pragma vertex vertexFunction 
				#pragma fragment fragmentFunction

				#include "UnityCG.cginc"

				struct vertexInput { 
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct v2f { // vertex to fragment struct
					float4 position : SV_POSITION;
					float2 uv : TEXCOORD0;
				};

				float4 _Color;
				sampler2D _MainTexture;

				v2f vertexFunction(vertexInput IN)
				{
					v2f OUT;

					OUT.position = UnityObjectToClipPos(IN.vertex);
					OUT.uv = IN.uv;

					return OUT;
				}

				float4 fragmentFunction(v2f IN) : SV_Target
				{
					float4 pixelColor = tex2D(_MainTexture, IN.uv);

					return pixelColor * _Color;
				}

				ENDCG
			}
		}
}
