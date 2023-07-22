Shader "Unlit/DiffusePerVertex"
{
	Properties{
		_DiffuseColor("DiffuseColor", Color) = (1.0, 1.0, 1.0, 1.0)
	}

	SubShader{

		Pass{

			Tags{ "LightModel" = "ForwardBase" }

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			#include "Lighting.cginc"

			float4 _DiffuseColor;

			struct a2v
			{
				float4 position : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 position : POSITION;
				float3 color : Color;
			};

			v2f vert(a2v v)
			{
				v2f o;
				o.position = UnityObjectToClipPos(v.position);

				float3 worldNormal = UnityObjectToWorldNormal(v.normal);
				float3 worldLight = normalize(_WorldSpaceLightPos0.xyz);

				//float3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;

				float3 diffuseColor = _LightColor0.rgb * _DiffuseColor.rgb * max(0, dot(worldNormal, worldLight));

				o.color = diffuseColor; //+ ambient;
				return o;
			}

			float4 frag(v2f i) : SV_Target
			{
				return float4(i.color, 1);
			}
			
			ENDCG
		}
	}
	
	Fallback "Diffuse"
}
