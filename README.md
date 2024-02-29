# Config


_[![Ulisses.Config NuGet Version](https://img.shields.io/nuget/v/Ulisses.Config.svg?style=for-the-badge&label=NuGet%3A%20Ulisses.Config)](https://www.nuget.org/packages/Ulisses.Config)_ 

![terminal_config](https://i.imgur.com/mHsJU4i.png)

 > Esse programa é um CLI, um Command Line Interface, um programa que roda pelo terminal exclusivamente pelo terminal.

O programa foi desenvolvido com o objetivo de testar a biblioteca Spectre.Console e implementar uma solução que permita uma geração fácil de valores de configurações para aplicações .Net, gerando connection strings e app settings.

[Fluxograma](https://whimsical.com/config-LCPMnRCLfhPL9kvzyhFxyt)

## Tecnologias

- .Net 8 e .Net 6
- [Spectre.Console](https://spectreconsole.net)


## Como usar 

Para utilizar o projeto é preciso ter as ferramentas SDKs do [.Net](https://dotnet.microsoft.com/pt-br/download). Após isso, execute o seguinte comando:

```bash
dotnet tool install --global Ulisses.Config
```

Com isso já deve ter a ferramenta disponível para uso no seu terminal. Use o comando a seguir para obter a ajuda.

```bash
config -h 
```

## Como Contribuir

Para contribuir com o projeto, você pode fazer uma sugestão para ser desenvolvida ou desenvolver você mesmo essa melhoria. Para sugerir 
utilize as **issues** para informar qual errou encontrou ou uma melhoria que gostaria que fosse implementada no app, tal sugestão pode levar 
a um **pull requests** para o desenvolvimento do que foi discutido na **issue**. Leia mais [aqui.](./CONTRIBUTING.md)



## Licença
Direitos Autorais © José Ulisses Silva Macedo Oliveira

O **config** é fornecido no estado em que se encontra sob a licença MIT. Para mais informações veja LICENÇA.

**Spectre.Console**, uma biblioteca na qual o **config** se baseia, é licenciada sob MIT quando distribuída como parte do **config**. 
- A Spectre.Console License cobre todos os outros usos, veja: https://github.com/spectreconsole/spectre.console/blob/main/LICENSE.md
