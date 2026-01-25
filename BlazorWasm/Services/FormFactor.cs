using System;
using GooginsTask01.Shared.Services;

namespace BlazorWasm.Services;

public class FormFactor : IFormFactor
{
    public string GetFormFactor()
    {
        return "Blazor Wasm";
    }

    public string GetPlatform()
    {
        return Environment.OSVersion.ToString();
    }
}
