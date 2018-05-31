Shader "Unlit/Outline"
{
	Properties
	{
		_Color ("Color", Color) = (1,1,1,1)
		_Outline("Outline", float) = 0
		_Size("Size", float) = 0
	}
	SubShader
	{
		Tags {"Queue"="Transparent" "RenderType"="Transparent"}
		
     	Blend SrcAlpha OneMinusSrcAlpha 
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
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 worldVertex : TEXCOORD1;
				float3 viewDir : TEXCOORD2;
				float3 worldNormal : NORMAL;
			};


			float4 _Color;
			float _Outline;
			float _Size;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.worldVertex = mul(unity_ObjectToWorld, v.vertex);
				o.worldNormal = UnityObjectToWorldNormal(v.normal);
				o.viewDir = normalize(UnityWorldSpaceViewDir(o.worldVertex.xyz));
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				half rim = 1.0-saturate(dot(i.viewDir, i.worldNormal));
				fixed4 col = _Color * pow (rim * _Size, _Outline);

				return col;
			}
			ENDCG
		}
	}
}
