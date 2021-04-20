// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "SS Cel Shader"
{
	Properties
	{
		_MainTex ( "Screen", 2D ) = "black" {}
		_ShadowOpacity("Shadow Opacity", Range( 0 , 1)) = 1
		_Mask("Mask", 2D) = "black" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

	}

	SubShader
	{
		LOD 0

		
		
		ZTest Always
		Cull Off
		ZWrite Off

		
		Pass
		{ 
			CGPROGRAM 

			

			#pragma vertex vert_img_custom 
			#pragma fragment frag
			#pragma target 3.0
			#include "UnityCG.cginc"
			

			struct appdata_img_custom
			{
				float4 vertex : POSITION;
				half2 texcoord : TEXCOORD0;
				
			};

			struct v2f_img_custom
			{
				float4 pos : SV_POSITION;
				half2 uv   : TEXCOORD0;
				half2 stereoUV : TEXCOORD2;
		#if UNITY_UV_STARTS_AT_TOP
				half4 uv2 : TEXCOORD1;
				half4 stereoUV2 : TEXCOORD3;
		#endif
				
			};

			uniform sampler2D _MainTex;
			uniform half4 _MainTex_TexelSize;
			uniform half4 _MainTex_ST;
			
			uniform sampler2D _CameraGBufferTexture3;
			uniform float4 _CameraGBufferTexture3_ST;
			uniform sampler2D _CameraGBufferTexture0;
			uniform float4 _CameraGBufferTexture0_ST;
			uniform sampler2D _CameraGBufferTexture1;
			uniform float4 _CameraGBufferTexture1_ST;
			uniform float _ShadowOpacity;
			uniform sampler2D _Mask;
			uniform float4 _Mask_ST;
			struct Gradient
			{
				int type;
				int colorsLength;
				int alphasLength;
				float4 colors[8];
				float2 alphas[8];
			};
			
			Gradient NewGradient(int type, int colorsLength, int alphasLength, 
			float4 colors0, float4 colors1, float4 colors2, float4 colors3, float4 colors4, float4 colors5, float4 colors6, float4 colors7,
			float2 alphas0, float2 alphas1, float2 alphas2, float2 alphas3, float2 alphas4, float2 alphas5, float2 alphas6, float2 alphas7)
			{
				Gradient g;
				g.type = type;
				g.colorsLength = colorsLength;
				g.alphasLength = alphasLength;
				g.colors[ 0 ] = colors0;
				g.colors[ 1 ] = colors1;
				g.colors[ 2 ] = colors2;
				g.colors[ 3 ] = colors3;
				g.colors[ 4 ] = colors4;
				g.colors[ 5 ] = colors5;
				g.colors[ 6 ] = colors6;
				g.colors[ 7 ] = colors7;
				g.alphas[ 0 ] = alphas0;
				g.alphas[ 1 ] = alphas1;
				g.alphas[ 2 ] = alphas2;
				g.alphas[ 3 ] = alphas3;
				g.alphas[ 4 ] = alphas4;
				g.alphas[ 5 ] = alphas5;
				g.alphas[ 6 ] = alphas6;
				g.alphas[ 7 ] = alphas7;
				return g;
			}
			
			float4 SampleGradient( Gradient gradient, float time )
			{
				float3 color = gradient.colors[0].rgb;
				UNITY_UNROLL
				for (int c = 1; c < 8; c++)
				{
				float colorPos = saturate((time - gradient.colors[c-1].w) / (gradient.colors[c].w - gradient.colors[c-1].w)) * step(c, (float)gradient.colorsLength-1);
				color = lerp(color, gradient.colors[c].rgb, lerp(colorPos, step(0.01, colorPos), gradient.type));
				}
				#ifndef UNITY_COLORSPACE_GAMMA
				color = half3(GammaToLinearSpaceExact(color.r), GammaToLinearSpaceExact(color.g), GammaToLinearSpaceExact(color.b));
				#endif
				float alpha = gradient.alphas[0].x;
				UNITY_UNROLL
				for (int a = 1; a < 8; a++)
				{
				float alphaPos = saturate((time - gradient.alphas[a-1].y) / (gradient.alphas[a].y - gradient.alphas[a-1].y)) * step(a, (float)gradient.alphasLength-1);
				alpha = lerp(alpha, gradient.alphas[a].x, lerp(alphaPos, step(0.01, alphaPos), gradient.type));
				}
				return float4(color, alpha);
			}
			


			v2f_img_custom vert_img_custom ( appdata_img_custom v  )
			{
				v2f_img_custom o;
				
				o.pos = UnityObjectToClipPos( v.vertex );
				o.uv = float4( v.texcoord.xy, 1, 1 );

				#if UNITY_UV_STARTS_AT_TOP
					o.uv2 = float4( v.texcoord.xy, 1, 1 );
					o.stereoUV2 = UnityStereoScreenSpaceUVAdjust ( o.uv2, _MainTex_ST );

					if ( _MainTex_TexelSize.y < 0.0 )
						o.uv.y = 1.0 - o.uv.y;
				#endif
				o.stereoUV = UnityStereoScreenSpaceUVAdjust ( o.uv, _MainTex_ST );
				return o;
			}

			half4 frag ( v2f_img_custom i ) : SV_Target
			{
				#ifdef UNITY_UV_STARTS_AT_TOP
					half2 uv = i.uv2;
					half2 stereoUV = i.stereoUV2;
				#else
					half2 uv = i.uv;
					half2 stereoUV = i.stereoUV;
				#endif	
				
				half4 finalColor;

				// ase common template code
				float2 uv_CameraGBufferTexture3 = i.uv.xy * _CameraGBufferTexture3_ST.xy + _CameraGBufferTexture3_ST.zw;
				float4 tex2DNode68 = tex2D( _CameraGBufferTexture3, uv_CameraGBufferTexture3 );
				float2 uv_CameraGBufferTexture0 = i.uv.xy * _CameraGBufferTexture0_ST.xy + _CameraGBufferTexture0_ST.zw;
				float4 tex2DNode65 = tex2D( _CameraGBufferTexture0, uv_CameraGBufferTexture0 );
				float2 uv_CameraGBufferTexture1 = i.uv.xy * _CameraGBufferTexture1_ST.xy + _CameraGBufferTexture1_ST.zw;
				float4 tex2DNode72 = tex2D( _CameraGBufferTexture1, uv_CameraGBufferTexture1 );
				float ifLocalVar77 = 0;
				if( tex2DNode72.a > 0.5 )
				ifLocalVar77 = 1.0;
				else if( tex2DNode72.a < 0.5 )
				ifLocalVar77 = 0.0;
				float4 lerpResult74 = lerp( tex2DNode65 , tex2DNode72 , ifLocalVar77);
				float4 lerpResult54 = lerp( tex2DNode68 , lerpResult74 , tex2DNode65.a);
				Gradient gradient58 = NewGradient( 0, 5, 2, float4( 0, 0, 0, 0.3441215 ), float4( 0.3098039, 0.3098039, 0.3098039, 0.3764706 ), float4( 0.3113208, 0.3113208, 0.3113208, 0.6764782 ), float4( 1, 1, 1, 0.7029374 ), float4( 1, 1, 1, 1 ), 0, 0, 0, float2( 1, 0 ), float2( 1, 1 ), 0, 0, 0, 0, 0, 0 );
				float3 desaturateInitialColor38 = tex2DNode68.rgb;
				float desaturateDot38 = dot( desaturateInitialColor38, float3( 0.299, 0.587, 0.114 ));
				float3 desaturateVar38 = lerp( desaturateInitialColor38, desaturateDot38.xxx, 1.0 );
				float3 desaturateInitialColor41 = lerpResult74.rgb;
				float desaturateDot41 = dot( desaturateInitialColor41, float3( 0.299, 0.587, 0.114 ));
				float3 desaturateVar41 = lerp( desaturateInitialColor41, desaturateDot41.xxx, 1.0 );
				float4 lerpResult62 = lerp( lerpResult54 , ( lerpResult54 * SampleGradient( gradient58, saturate( ( desaturateVar38 / desaturateVar41 ) ).x ) ) , _ShadowOpacity);
				float4 color96 = IsGammaSpace() ? float4(0.2236116,0.2948118,0.6320754,0) : float4(0.04094418,0.07069632,0.3572768,0);
				float3 desaturateInitialColor94 = lerpResult62.rgb;
				float desaturateDot94 = dot( desaturateInitialColor94, float3( 0.299, 0.587, 0.114 ));
				float3 desaturateVar94 = lerp( desaturateInitialColor94, desaturateDot94.xxx, 1.0 );
				float2 uv_Mask = i.uv.xy * _Mask_ST.xy + _Mask_ST.zw;
				float ifLocalVar88 = 0;
				if( tex2D( _Mask, uv_Mask ).r > 0.0 )
				ifLocalVar88 = 1.0;
				else if( tex2D( _Mask, uv_Mask ).r < 0.0 )
				ifLocalVar88 = 0.0;
				float4 lerpResult92 = lerp( lerpResult62 , ( color96 * float4( desaturateVar94 , 0.0 ) ) , ifLocalVar88);
				

				finalColor = lerpResult92;

				return finalColor;
			} 
			ENDCG 
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=18000
804;73;804;629;-554.205;1926.599;3.051059;True;False
Node;AmplifyShaderEditor.TexturePropertyNode;71;-1284.294,-1395.686;Inherit;True;Global;_CameraGBufferTexture1;_CameraGBufferTexture1;2;0;Create;True;0;0;False;0;None;None;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.TexturePropertyNode;66;-834.3839,-936.1281;Inherit;True;Global;_CameraGBufferTexture0;_CameraGBufferTexture0;0;0;Create;True;0;0;False;0;None;None;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SamplerNode;72;-928.061,-1377.947;Inherit;True;Global;_TextureSample0;Texture Sample 0;2;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;78;-681.5717,-1167.676;Inherit;False;Constant;_Float0;Float 0;5;0;Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;79;-714.1156,-1099.102;Inherit;False;Constant;_Float1;Float 1;5;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;80;-733.8744,-1024.716;Inherit;False;Constant;_Float2;Float 2;5;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;65;-478.1516,-918.3895;Inherit;True;Global;TextureSample1;Texture Sample 1;3;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ConditionalIfNode;77;-500.5537,-1124.045;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;70;-1066.641,-408.8168;Inherit;True;Global;_CameraGBufferTexture3;_CameraGBufferTexture3;1;0;Create;True;0;0;False;0;None;None;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.LerpOp;74;-31.38661,-1342.894;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;68;-659.2992,-335.0891;Inherit;True;Global;TextureSample0;Texture Sample 0;1;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DesaturateOpNode;41;33.9773,-485.8192;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;1;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DesaturateOpNode;38;44.14423,-303.5383;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;1;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;42;276.4668,-379.4032;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SaturateNode;44;431.6238,-384.8529;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GradientNode;58;578.9501,-1053.199;Inherit;False;0;5;2;0,0,0,0.3441215;0.3098039,0.3098039,0.3098039,0.3764706;0.3113208,0.3113208,0.3113208,0.6764782;1,1,1,0.7029374;1,1,1,1;1,0;1,1;0;1;OBJECT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;47;576.1827,-414.6826;Inherit;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.LerpOp;54;401.1635,-878.7625;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GradientSampleNode;59;943.7712,-967.5123;Inherit;True;2;0;OBJECT;;False;1;FLOAT;0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;61;840.3228,-1083.144;Inherit;False;Property;_ShadowOpacity;Shadow Opacity;3;0;Create;True;0;0;False;0;1;0.7;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;63;1255.341,-1182.492;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TexturePropertyNode;81;1203.716,-757.2645;Inherit;True;Property;_Mask;Mask;4;0;Create;True;0;0;False;0;811d4d2faefe24a4f919062698f7dedf;None;False;black;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.LerpOp;62;1435.617,-951.3478;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;82;1523.942,-658.7853;Inherit;True;Property;_TextureSample1;Texture Sample 1;5;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;89;1734.292,-452.3991;Inherit;False;Constant;_Float3;Float 3;5;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;90;1750.099,-365.459;Inherit;False;Constant;_Float4;Float 4;5;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;91;1783.296,-295.9074;Inherit;False;Constant;_Float5;Float 5;5;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DesaturateOpNode;94;1571.328,-1111.341;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;1;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;96;1393.918,-1307.123;Inherit;False;Constant;_Color0;Color 0;5;0;Create;True;0;0;False;0;0.2236116,0.2948118,0.6320754,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ConditionalIfNode;88;2006.72,-636.259;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;95;1600.918,-1214.123;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT3;0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ScreenColorNode;93;1723.182,-1368.137;Inherit;False;Global;_GrabScreen0;Grab Screen 0;5;0;Create;True;0;0;False;0;Object;-1;False;False;1;0;FLOAT2;0,0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;87;1599.347,-810.3551;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;92;1922.714,-1074.997;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;37;2192.191,-1029.872;Float;False;True;-1;2;ASEMaterialInspector;0;2;SS Cel Shader;c71b220b631b6344493ea3cf87110c93;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;1;False;False;False;True;2;False;-1;False;False;True;2;False;-1;True;7;False;-1;False;True;0;False;0;False;False;False;False;False;False;False;False;False;False;True;2;0;;0;0;Standard;0;0;1;True;False;;0
WireConnection;72;0;71;0
WireConnection;65;0;66;0
WireConnection;77;0;72;4
WireConnection;77;1;78;0
WireConnection;77;2;79;0
WireConnection;77;4;80;0
WireConnection;74;0;65;0
WireConnection;74;1;72;0
WireConnection;74;2;77;0
WireConnection;68;0;70;0
WireConnection;41;0;74;0
WireConnection;38;0;68;0
WireConnection;42;0;38;0
WireConnection;42;1;41;0
WireConnection;44;0;42;0
WireConnection;47;0;44;0
WireConnection;54;0;68;0
WireConnection;54;1;74;0
WireConnection;54;2;65;4
WireConnection;59;0;58;0
WireConnection;59;1;47;0
WireConnection;63;0;54;0
WireConnection;63;1;59;0
WireConnection;62;0;54;0
WireConnection;62;1;63;0
WireConnection;62;2;61;0
WireConnection;82;0;81;0
WireConnection;94;0;62;0
WireConnection;88;0;82;1
WireConnection;88;1;89;0
WireConnection;88;2;90;0
WireConnection;88;4;91;0
WireConnection;95;0;96;0
WireConnection;95;1;94;0
WireConnection;92;0;62;0
WireConnection;92;1;95;0
WireConnection;92;2;88;0
WireConnection;37;0;92;0
ASEEND*/
//CHKSM=E00315F3FD0F5E7859801F51AFFA710A87B9B4F2