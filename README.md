# MyanmarNameology

မြန်မာနာမည်ကိန်းတွက်စက်ကို Blazor WebAssembly နဲ့ တည်ဆောက်ထားတဲ့ compact web app ဖြစ်ပါတယ်။ နာမည်ထည့်ပြီး မြန်မာအက္ခရာနေ့နံတန်ဖိုးကို တွက်ကာ `၇ ပေါင်း`၊ `၉ နဲ့စား` ပြီး အကြွင်းကိန်းအလိုက် ရလဒ်အဓိပ္ပာယ်ကို ပြပေးပါတယ်။

> ဒီ project က မြန်မာ့ရိုးရာ အမည်ဗေဒင်ယုံကြည်မှုအပေါ် အခြေခံထားတဲ့ entertainment calculator ဖြစ်ပြီး သိပ္ပံနည်းကျ အတည်ပြုထားတဲ့ တွက်ချက်နည်းမဟုတ်ပါ။

## Project Overview

- Framework: Blazor WebAssembly standalone
- Target: .NET 10 (`net10.0`)
- App route: `/`
- Main UI: `MyanmarNameology/Pages/Home.razor`
- Calculator logic: `MyanmarNameology/Services/NameologyCalculator.cs`
- Result models: `MyanmarNameology/Models/NameologyResult.cs`
- Main icon/logo: `MyanmarNameology/wwwroot/logo.svg`

ဒီ app ရဲ့ landing page က marketing page မဟုတ်ဘဲ calculator ကိုပဲ ပထမဆုံး screen အနေနဲ့ သုံးနိုင်အောင် ဖန်တီးထားပါတယ်။ Name input, calculate action, live result area, formula summary, နေ့နံတန်ဖိုး table တွေကို responsive layout နဲ့ ပြထားပါတယ်။

## App Flow

1. User က မြန်မာနာမည်ကို input box ထဲမှာ ရိုက်ထည့်သည်။
2. `Home.razor` က input ပြောင်းသည့်အခါတိုင်း `NameologyCalculator.Calculate(...)` ကို ခေါ်သည်။
3. Calculator က နာမည်ထဲက တွက်နိုင်တဲ့ မြန်မာအက္ခရာများကို ရွေးထုတ်သည်။
4. အသတ်နဲ့ဆုံးတဲ့ final consonant များကို keyword ထဲမထည့်ဘဲ ကျော်ထားသည်။
5. ရွေးထုတ်ထားတဲ့ အက္ခရာများကို နေ့နံတန်ဖိုးနဲ့ map လုပ်ပြီး စုစုပေါင်းတွက်သည်။
6. စုစုပေါင်းကို `၇` ပေါင်းပြီး `၉` နဲ့စားသည်။
7. အကြွင်း `၀` ဖြစ်ရင် `၉` အဖြစ်ယူပြီး ကိန်းအဓိပ္ပာယ်ကို ပြသည်။

တွက်ချက်ပုံအကျဉ်း:

```text
အက္ခရာတန်ဖိုး စုစုပေါင်း + ၇
ရလာဒ်ကို ၉ နဲ့စား
အကြွင်းကိန်းအလိုက် အဓိပ္ပာယ်ပြ
```

## နေ့နံတန်ဖိုး Table

| နေ့နံ | တန်ခိုး | နံတန်ဖိုး |
| --- | --- | --- |
| တနင်္ဂနွေ | အ ဣ ဤ ဥ ဦ ဧ ဩ ဪ | ၁ |
| တနင်္လာ | က ခ ဂ ဃ င | ၂ |
| အင်္ဂါ | စ ဆ ဇ ဈ ည | ၃ |
| ဗုဒ္ဓဟူး | ယ ရ လ ဝ | ၄ |
| ကြာသပတေး | ပ ဖ ဗ ဘ မ | ၅ |
| သောကြာ | သ ဟ ဠ | ၆ |
| စနေ | တ ထ ဒ ဓ န | ၇ |

## အသုံးပြုနည်း

1. Website ကိုဖွင့်ပါ။
2. နာမည် input ထဲမှာ မိမိတွက်ချင်တဲ့ မြန်မာနာမည်ကို ရိုက်ထည့်ပါ။
3. Result area မှာ အမည်ကီး၊ အက္ခရာတန်ဖိုးခွဲခြမ်းချက်၊ စုစုပေါင်း၊ `၇ ပေါင်းပြီး` တန်ဖိုး၊ `၉ နဲ့စား` အကြွင်းနဲ့ ကိန်းအဓိပ္ပာယ်ကို ကြည့်နိုင်ပါတယ်။
4. မြန်မာအက္ခရာမပါသော input ဖြစ်ရင် app က တွက်လို့မရကြောင်း friendly message ပြပေးပါတယ်။
5. Empty input ဖြစ်ရင် နာမည်ထည့်ရန် prompt ပြပေးပါတယ်။

## Development

လိုအပ်ချက်:

- .NET 10 SDK

Restore:

```powershell
dotnet restore MyanmarNameology.slnx
```

Run local app:

```powershell
dotnet run --project MyanmarNameology --urls http://localhost:5166
```

ပြီးရင် browser မှာ ဖွင့်ပါ:

```text
http://localhost:5166/
```

Build:

```powershell
dotnet build MyanmarNameology.slnx
```

## Project Structure

```text
MyanmarNameology/
├─ MyanmarNameology.slnx
├─ vercel.json
├─ MyanmarNameology/
│  ├─ Pages/Home.razor
│  ├─ Services/NameologyCalculator.cs
│  ├─ Models/NameologyResult.cs
│  ├─ wwwroot/index.html
│  ├─ wwwroot/logo.svg
│  └─ wwwroot/css/app.css
└─ .github/workflows/deploy-vercel.yml
```

## Deploy Flow

GitHub Actions workflow က Vercel ကို deploy လုပ်ဖို့ ပြင်ထားပါတယ်။ Repository secrets ထဲမှာ အောက်ပါ values တွေလိုအပ်ပါတယ်။

```text
VERCEL_TOKEN
VERCEL_ORG_ID
VERCEL_PROJECT_ID
```

`main` branch ကို push လုပ်တဲ့အခါ build output ကို Vercel project သို့ deploy လုပ်နိုင်ပါတယ်။ SPA routing အတွက် `vercel.json` ထဲမှာ rewrite rule ပါဝင်ပြီး Blazor `_framework` assets တွေကို long-term cache header သတ်မှတ်ထားပါတယ်။
