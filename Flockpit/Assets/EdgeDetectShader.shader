Shader "Hidden/EdgeDetectShader"
//https://www.alanzucconi.com/2015/07/08/screen-shaders-and-postprocessing-effects-in-unity3d/
//https://www.youtube.com/watch?v=C0uJ4sZelio
//:https://homepages.inf.ed.ac.uk/rbf/HIPR2/sobel.htm
//Example of sobel filter and using sobel filter
//Zink.J, Pettineo.M, Hoxley.J(2011).Practical Rendering and Computation with Direct3D 11
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

			float findIntensity(float4 colour) {
				return (colour.x + colour.y + colour.z) / 3;
			}

            sampler2D _MainTex;

            fixed4 frag (v2f input) : SV_Target
            {
				float2 resolution = float2(964, 538);
				float2 texUnit = float2(1.0f / resolution.x, 1.0f / resolution.y);
                fixed4 textureColour = tex2D(_MainTex, input.uv);
				float solbelX[3][3] = { {-1,-2,-1},{0,0,0},{1,2,1} };
				float pixelSample[9] = {0,0,0,0,0,0,0,0,0};
				float gradientX, gradientY;
				float gradientXY;
				//
				pixelSample[0] = findIntensity(tex2D(_MainTex, float2(input.uv.x - 1 * texUnit.x, input.uv.y - 1 * texUnit.y)));
				pixelSample[1] = findIntensity(tex2D(_MainTex, float2(input.uv.x, input.uv.y - 1 * texUnit.y)));
				pixelSample[2] = findIntensity(tex2D(_MainTex, float2(input.uv.x + 1 * texUnit.x, input.uv.y - 1 * texUnit.y)));
				pixelSample[3] = findIntensity(tex2D(_MainTex, float2(input.uv.x - 1 * texUnit.x, input.uv.y)));
				pixelSample[4] = findIntensity(tex2D(_MainTex, float2(input.uv.x + 1 * texUnit.x, input.uv.y)));
				pixelSample[5] = findIntensity(tex2D(_MainTex, float2(input.uv.x - 1 * texUnit.x, input.uv.y + 1 * texUnit.y)));
				pixelSample[6] = findIntensity(tex2D(_MainTex, float2(input.uv.x, input.uv.y + 1 * texUnit.y)));
				pixelSample[7] = findIntensity(tex2D(_MainTex, float2(input.uv.x+1 * texUnit.x, input.uv.y + 1 * texUnit.y)));
				gradientX = pixelSample[0] + 2.0 * pixelSample[3] + pixelSample[5] - pixelSample[2] - 2.0 * pixelSample[4] - pixelSample[7];
				gradientY = pixelSample[0] + 2.0 * pixelSample[1] + pixelSample[2] - pixelSample[5] - 2.0 * pixelSample[6] - pixelSample[7];
				gradientXY = sqrt(pow(gradientX, 2) + pow(gradientY, 2));
				if (gradientXY > 0.2) {
					textureColour = float4(1.0f, 1.0f, 1.0f, 1.0f);
				}
				else {
					textureColour = float4(0.0f, 0.0f, 0.0f, 1.0f);
				}
				textureColour = float4(gradientXY, gradientXY, gradientXY, 1.0f);
                //
				float intensityOne = (textureColour.x + textureColour.y + textureColour.z) / 3;
				float4 baseCol = tex2D(_MainTex, input.uv);
				float intensityTwo = (baseCol.x + baseCol.y + baseCol.z) / 3;
				float outlineIntensity = 0.75f;
				if (intensityOne * outlineIntensity < intensityTwo) {
					return baseCol;
				}
				else {
					return float4(0.0f, 0.0f, 0.0f, 1.0f);
				}
				//return textureColour;
            }
            ENDCG
        }
    }
}
