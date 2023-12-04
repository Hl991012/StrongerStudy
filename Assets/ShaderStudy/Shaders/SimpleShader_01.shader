Shader "Study/SimpleShader_01"
{
    Properties{
        //声明一个Color类型的属性
        _Color ("Color Tint", Color) = (1.0, 1.0, 1.0, 1.0)   
    }
    
    SubShader
    {
        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            fixed4 _Color;

            struct a2v
            {
                //POSITION:告诉Unity使用模型空间的顶点坐标去填充vertex
                //NORMAL：告诉Unity使用模型空间的法线方向填充normal变量
                //TEXCOORD0：告诉Unity使用模型的第一套纹理空间去填充texCoord变量
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 texCoord : TEXCOORD0;
            };

            struct v2f
            {
                //SV_POSITION：告诉Unity，pos里面包含了顶点在裁剪空间的位置信息
                //COLOR0：用于存储颜色信息
                float4 position : SV_POSITION;
                fixed3 color : COLOR0;
            };

             v2f vert(a2v v) {
                 v2f o;
                 o.position =  UnityObjectToClipPos(v.vertex);
                 o.color = v.normal * 0.5 + fixed3(0.5, 0.5, 0.5);
                 return o;
             }

            fixed4 frag(v2f i):SV_Target
            {
                fixed3 c = i.color;
                c*=_Color.rgb;
                return fixed4(c, 1.0);
            }
            
            ENDCG
        }
    }
}
