Shader "Study/序列帧动画"
{
    Properties
    {
        _Color("Color", Color) = (1, 1, 1, 1)
        _MainTex("MainTex", 2D) = "White" {}
        _HorizontalAmount("HorizontalAmount", float) = 4
        _Vertical("Vertical", float) = 4
        _Speed("Speed", float) = 1
    }   
    
    SubShader
    {
        Tags{"Queue" = "Transparent" "IgnoreProjector" = "true" "RenderType" = "Transparent"}
        
        Pass{
            Tags{"LightMode" = "forwardBase"}
            
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            
            CGPROGRAM

            #pragma vertex vert;
            #pragma fragment frag;

            #include "UnityCG.cginc"

            float4 _Color;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _HorizontalAmount;
            float _Vertical;
            float _Speed;

            struct a2v
            {
                float4 vert : POSITION;
                float4 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(a2v v)
            {
                v2f o;
                o.pos = mul(unity_MatrixMVP, v.vert);
                o.uv = v.texcoord * _MainTex_ST.xy + _MainTex_ST.zw;
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                float time = floor(_Time.y * _Speed);
                float2 uv = float2(i.uv.x / _HorizontalAmount, i.uv.y / _Vertical);
                return float4(1, 1, 1, 1);
            }
            
            ENDCG
        }    
    } 
}