Shader "Unlit/001"
{
    Properties
    {
        // 漫反射颜色
        _Diffuse ("_Diffuse",Color) = (1,1,1,1)
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            
            float4 _Diffuse;
            struct v2v
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
 
            struct v2f
            {
                float4 vertex : POSITION;
                float3 color : Color;
            };
 
            v2f vert (v2v v)
            {
                v2f o;
                // 将对象空间中的点变换到齐次坐标中的摄像机裁剪空间
                o.vertex = UnityObjectToClipPos(v.vertex);
                // 光源方向
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                // 将法向量转到世界坐标法向量
                float3 normal = UnityObjectToWorldNormal(v.normal);
                // 环境光颜色
                float3 ambinet = UNITY_LIGHTMODEL_AMBIENT.xyz;
                // 光源颜色 0 表示第一套光源 ，场景里可以有多个光源
                float3 lightColor = _LightColor0.xyz;
                // 漫反射公式 计算
                float3 diffuse = _Diffuse * lightColor * max(0,dot(lightDir,normal));
                o.color = diffuse + ambinet;
                return o;   
            }
 
            fixed4 frag (v2f i) : SV_Target
            {
                float4 color = float4(i.color,1);
                return color;
            }
            ENDCG
        }
    }
}