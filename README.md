# Gerador de Código de Barras - Padrão ITF-14
O intuito desse projeto é exemplificar o algoritmo para criação de código de barras no padrão ITF-14.

O manual que detalha o padrão do código de barras está anexo e também pode ser obtido no seguinte endereço:
http://www.gs1.org/docs/barcodes/GS1_General_Specifications.pdf.
O padrão ITF-14 é detalhado a partir da página 230.
O mesmo manual e lógica de código podem ser utilizados para implementar o Padrão ITF-13.

# Funcionamento
Na solution há dois projetos, um implementado em C# e o outro em VB.Net. Ambos são idênticos.

Ao executar o projeto, é necessário informar um código de 14 dígitos numéricos. 
O sistema realiza validação do código informado utilizando o algoritmo do módulo 10.

A partir do código numérico informado, uma imagem do código de barras é gerada. Esta imagem pode ser utilizada em layouts de etiquetas, por exemplo.
