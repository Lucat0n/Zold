sampler s0;  
	float param2;	
    texture lightMask;  
    sampler lightSampler = sampler_state{
        Texture = <lightMask>;
        };  
      
    float4 PixelShaderLight(float2 coords: TEXCOORD0) : COLOR0  
    {  
        float4 color = tex2D(s0, coords);  
        float4 lightColor = tex2D(lightSampler, coords);  
        return color * lightColor *color.rgba *0.4;  
    }  

	      
    technique Technique1  
    {  
        pass Pass1  
        {  
            PixelShader = compile ps_2_0 PixelShaderLight();  
        }  
    }  