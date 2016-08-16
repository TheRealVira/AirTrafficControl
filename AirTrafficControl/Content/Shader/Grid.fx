#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

sampler TextureSampler : register(s0);

matrix WorldViewProjection;
float2 ScreenCoords;
float4 Color1;
float4 Color2;
float Div;

struct VertexShaderOutput
{
	float4 Position : TEXCOORD0;
	float4 Color : COLOR0;
};

float4 MainPS(VertexShaderOutput input) : COLOR
{
	float x,y;
	x = frac(input.Position.x*(ScreenCoords.x / Div));
	y = frac(input.Position.y*(ScreenCoords.y / Div));

	if (x > 0.9 || y > 0.9) {
		return Color1;
	}
	else
	{
		return Color2;
	}
}

technique GridDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};