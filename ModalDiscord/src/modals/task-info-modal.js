export async function taskInfoModal(interaction) {
    console.log('Task info modal submitted');
    // Como � apenas visualiza��o, n�o faz nada por enquanto
    await interaction.reply({
        content: 'Informa��es da task visualizadas!',
        ephemeral: true,
    });
}