Shader "Unlit/HexShad"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
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

			#define PI 3.1415926


			float tri(float a)
			{    
				return abs(1.0- fmod(2.0*a+1.0,2.0));
			}

			float stp(float a)
			{
				return fmod(a,2.0) - frac(a);
			}



			float aIsGreater(float A, float B)
			{
   				float diff = A-B;
   				return 0.5+0.5*abs(diff)/(diff);
			}



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
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}

			
			fixed4 frag (v2f i) : SV_Target
			{

				float numHexWide = 8.5;//18.5;

				float4 fragColor;
				float2 adjUV = i.uv.xy;
    


				float2 tuv = adjUV*numHexWide;
				tuv.x += 0.5*stp(adjUV.y*numHexWide*.5);

				float2 tempuv = floor(tuv)/numHexWide;
				tempuv.y = floor(tuv.y*0.5)/numHexWide;
    
				float trimod = aIsGreater(frac(tuv.y), tri(tuv.x));
				float2 triuv = float2(tuv.x - (0.5 - stp(tuv.y*0.5+0.5 )) , tuv.y*0.5 + 0.5);
				triuv = floor(triuv)/numHexWide;
    
   				tempuv = lerp(tempuv, triuv , trimod*fmod(floor(tuv.y), 2.0));



				float4 col = float4(tempuv.x, tempuv.y , 0.1+0.2*sin(_Time.y*0.5),1.0); 
				tempuv.y *=2.0;
   
				float4 rCol = float4(.9,0,0,1);
				float4 gCol = float4(.1,1,0,1);
				float4 bCol = float4(0,0,1,1);


				fragColor =	lerp(float4(0,0,0,0), rCol, col.r) + 
									lerp(float4(0,0,0,0), gCol, col.g) + 
									lerp(float4(0,0,0,0), bCol, col.b) + 
									float4(0,0,0,1); 


				return fragColor;
			}
			ENDCG
		}
	}
}
