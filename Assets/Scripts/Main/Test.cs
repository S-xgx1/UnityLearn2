#region

using ASPDotnetLearn.Shared;
using Cysharp.Net.Http;
using Cysharp.Threading.Tasks;
using MagicOnion;
using MagicOnion.Client;
using MagicOnion.Unity;
using UnityEngine;

#endregion

public class Test : MonoBehaviour
{
    void Start()
    {
        _ = In();
        return;

        async UniTask In()
        {
            print("start0");
            var channel = GrpcChannelx.ForAddress("https://localhost:5001");
            print("start1");
            var client = MagicOnionClient.Create<IMyFirstService>(channel);
            print("start2");
            var result = await client.SumAsync(123, 456);
            print($"Result: {result}");
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void OnRuntimeInitialize()
    {
        GrpcChannelProviderHost.Initialize(new DefaultGrpcChannelProvider(() => new()
        {
                HttpHandler = new YetAnotherHttpHandler
                {
                        Http2Only = true,
                        RootCertificates = @"
-----BEGIN CERTIFICATE-----
MIIDDDCCAfSgAwIBAgIIS7/qtsP3bm0wDQYJKoZIhvcNAQELBQAwFDESMBAGA1UE
AxMJbG9jYWxob3N0MB4XDTI0MDgxMDEyNTQzN1oXDTI1MDgxMDEyNTQzN1owFDES
MBAGA1UEAxMJbG9jYWxob3N0MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKC
AQEAoo1DPxIHUzjVf9aARTTr8GRvRNrC9Xq4fiq4BraT2eTXoK4a23YABwRc6XZN
dPOVjJp7zQQq7YgXRuSgZCv+1gDGFM+tskKysKJb28E7/uljl54MZI90OFNcUw3O
AcvopIJrtmE4TK0klZVIGrooXdtOBqScxFZmR1DjwP6blFizufjhYZafsTrj2EfR
CDG/l/w/cSySwwvz9A/z9+pR72ziepM3ujTKVG1BvUl5MfPCSdecM+D1mhOKKRkm
k5Gdn7g8vgWQHGQjmEnKXfoDJC8fb25vOV1fWRMcLXTa5K009CAUX8CGaySamehY
nz80ThvD4VgWK5YEsaB+UDoBKQIDAQABo2IwYDAMBgNVHRMBAf8EAjAAMA4GA1Ud
DwEB/wQEAwIFoDAWBgNVHSUBAf8EDDAKBggrBgEFBQcDATAXBgNVHREBAf8EDTAL
gglsb2NhbGhvc3QwDwYKKwYBBAGCN1QBAQQBAjANBgkqhkiG9w0BAQsFAAOCAQEA
nCZdM3dgC338S0CgzHRU5zPz/kUtF0JpG3fzS7o2UFQ1S9cyTgSL3nJ38RZAmiEe
dm7U86dJPZ7jm4qoyv3HR1xVaqkayMoBWTLXQR+ypInAylZoi8V7lmIt5dLAs3eZ
61UI4UQGBz/bmodb7+NrWz43noUaKZynLkLxruNr6ALM567vMz9c48O0ncIVvkzN
ZAVglM/hkgS+yjfCaBIqqT2KtPC7PBWNRbKEV65U/bV6HNbyxIvjBbALgyGSFBkY
iPDLdxre8zpePWCS8f617pdHOek4y3q1Ek7BU1AoXyikzaMPf14UGuTG9yaBP48c
Phxop88ZsltKruKy5s53kA==
-----END CERTIFICATE-----
",
                },
                DisposeHttpClient = true,
        }));
    }
}