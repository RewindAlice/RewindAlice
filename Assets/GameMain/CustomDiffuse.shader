Shader "Custom/PlayerDiffuse" {
    Properties {
        _NotVisibleColor ("NotVisibleColor (RGB)", Color) = (0.3,0.3,0.3,1)
		_MainTex ("Base (RGB) Trans (A)", 2D) = "white" { }
		_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }

    SubShader {
        Tags { "Queue" = "Geometry+500" "RenderType"="Opaque" }
        LOD 200
		Cull off
        Pass {
			//描画を手前にする
            ZTest Greater
            //Lighting Off
            ZWrite Off
            //Color [_NotVisibleColor]
        }

        Pass {
            //ZTest LEqual
            Material {
                Diffuse (1,1,1,1)
                Ambient (1,1,1,1)
            }
            //Lighting On
            SetTexture [_MainTex] { combine texture } 
        }

    }

    FallBack "Diffuse"
}
