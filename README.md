# Config

![terminal_config](https://github.com/jos3s/config/assets/50359547/6760e72b-da75-4031-b757-dcd7c977816a)


 > Esse programa é um CLI, um Command Line Interface, um programa que roda pelo terminal exclusivamente pelo terminal.

O programa foi desenvolvido com o objetivo de testar a biblioteca Spectre.Console e implementar uma solução que permita uma geração fácil de valores de configurações para aplicações .Net, gerando connection strings e app settings.

[Fluxograma](https://whimsical.com/config-LCPMnRCLfhPL9kvzyhFxyt)

## Tecnologias

- .Net 7
- [Spectre.Console](https://spectreconsole.net)

## Como Usar

```bash
gh repo clone jos3/config

---

git clone https://github.com/jos3s/config.git

```

Abra a pasta do projeto e execute o comando no terminal para executar o pograma e exibir a documentação.

```bash
dotnet run --property WarningLevel=0 -- -h
```

## Licença
Direitos Autorais © José Ulisses Silva Macedo Oliveira

O **config** é fornecido no estado em que se encontra sob a licença MIT. Para mais informações veja LICENÇA.

**Spectre.Console**, uma biblioteca na qual o **config** se baseia, é licenciada sob MIT quando distribuída como parte do **config**. 
- A Spectre.Console License cobre todos os outros usos, veja: https://github.com/spectreconsole/spectre.console/blob/main/LICENSE.md
