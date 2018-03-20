Shader "Volume/Volume Unlit"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
        _Displacement("Displacement", Range(0.0, 500.0)) = 5.0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 600
        Cull Off

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
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

            //Uniforms
			sampler2D _MainTex;
			float4 _MainTex_ST;
            float _Displacement;

            //Vertex shader
			v2f vert (appdata v)
			{   
                //Split the UV so we only use the lower half
                fixed4 depthUV = fixed4(v.uv.x, v.uv.y, 0, 0);
                depthUV.y *= 0.5;

                //Sample the depth map
                fixed4 depthSample = tex2Dlod(_MainTex, depthUV);
                v.vertex.z -= (-depthSample.r * _Displacement);
                //Pack all the data for the fragment shader
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}

            //Fragment shader
			fixed4 frag (v2f i) : SV_Target
			{
                //Split the UV's so we read the upper half
                fixed2 colorUV = i.uv;
                colorUV.y *= 0.5;
                colorUV.y += 0.5;

				// sample the texture
				fixed4 col = tex2D(_MainTex, colorUV);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
            
			ENDCG
		}
	}
}
