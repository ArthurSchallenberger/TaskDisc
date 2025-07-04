export async function taskInfoModal(interaction) {
    console.log('Task info modal submitted');
    // Como é apenas visualização, não faz nada por enquanto
    await interaction.reply({
        content: 'Informações da task visualizadas!',
        ephemeral: true,
    });
}