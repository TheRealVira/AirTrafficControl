#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

sampler TextureSampler : register(s0);

float2 RumbleVectorR;
float2 RumbleVectorG;
float2 RumbleVectorB;
struct VertexShaderOutput
{
	float4 Position : TEXCOORD0;
	float4 Color : COLOR0;
};

float4 MainPS(VertexShaderOutput input) : COLOR
{
	float3 colorR = tex2D(TextureSampler, input.Position + RumbleVectorR);
	float3 colorG = tex2D(TextureSampler, input.Position + RumbleVectorG);
	float3 colorB = tex2D(TextureSampler, input.Position + RumbleVectorB);

	float4 returnColor;
	returnColor.r = colorR.r;
	returnColor.g = colorG.g;
	returnColor.b = colorB.b;
	returnColor.a = 1.0f;
	return returnColor;
}

technique RetroDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};