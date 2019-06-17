sampler s0;
float param1;
float4 PixelShaderFunction(float2 coords: TEXCOORD0) : COLOR0
{
   float4 color = tex2D(s0, coords );    
   return color.rgba * param1;
}

technique Technique1
{
	pass Pass1
	{
		PixelShader = compile ps_2_0 PixelShaderFunction();
	}
}