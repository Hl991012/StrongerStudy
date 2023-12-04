Shader"Study/NormalMap"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("MainTex", 2D) = "White"{}
        _BumpMap("NormalMap", 2D) = "bump"{}
        _BumpScale("BumpScale", float) = 1.0
        _Specular("Specular", Color) = (1,1,1,1)
        _Gloss("Gloss", float) = 1.0
    }
    
    SubShader{
        Pass{
            Tags {"LightModel" = "ForwardBase"}
            
            CGPROGRAM
            
            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            #pragma vertex vert;
            #pragma fragment frag;

            float4 _Color;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _BumpMap;
            float4 _BumpMap_ST;
            float _BumpScale;
            float4 _Specular;
            float _Gloss;

            struct a2v
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                //告诉Unity将顶点的切线方向填充进来,类型是float4，我们需要w分量来计算切线空间的第三个坐标轴-副切线的方向性
                float4 tangent : TANGENT;
                float4 texCoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float4 uv : TEXCOORD0;
                //存储变化后的光照方向
                float3 lightDir : TEXCOORD1;
                //存储变换后的观察方向
                float3 viewDir : TEXCOORD2;
            };

            v2f vert(a2v v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);

                o.uv.xy = v.texCoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
                o.uv.zw = v.texCoord.xy * _BumpMap_ST.xy + _BumpMap_ST.zw;

                //计算副法线
                float3 biNormal = cross(normalize(v.tangent.xyz), normalize(v.normal)) * v.tangent.w;
                TANGENT_SPACE_ROTATION;
                o.lightDir = normalize(mul(rotation, ObjSpaceLightDir(v.vertex)).xyz);
                o.viewDir = normalize(mul(rotation, ObjSpaceViewDir(v.vertex)).xyz);
                
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                float4 packedNormal = tex2D(_BumpMap, i.uv.zw);
                float3 tangentNormal = UnpackNormal(packedNormal);
                tangentNormal.xy *= _BumpScale;
                tangentNormal.z = sqrt(1.0 - saturate(dot(tangentNormal.xy, tangentNormal.xy)));

                float3 albedo = tex2D(_MainTex, i.uv.xy).rgb * _Color;
                //计算环境光
                float3 ambient = UNITY_LIGHTMODEL_AMBIENT * albedo;
                //计算漫反射颜色
                float3 diffuse = _LightColor0.rgb * albedo * max(0, dot(tangentNormal, i.lightDir));
                //计算高光
                float3 halfDir = normalize(i.lightDir + i.viewDir);
                float3 specular = _Specular.rgb * _LightColor0.rgb * pow(max(0, dot(tangentNormal, halfDir)), _Gloss);

                return float4(ambient + diffuse + specular, 1);
            }
            
            ENDCG
        }    
    }
}