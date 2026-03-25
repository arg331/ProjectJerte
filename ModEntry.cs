using System;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Characters;

namespace ProjectJerte
{
    public class ModEntry : Mod
    {
        public override void Entry(IModHelper helper)
        {
            // Enganchamos el registro del comando cuando el juego termina de inicializarse
            helper.Events.GameLoop.GameLaunched += OnGameLaunched;
        }

        private void OnGameLaunched(object sender, GameLaunchedEventArgs e)
        {
            // Inyectamos nuestro comando en el diccionario interno de eventos del juego
            Event.RegisterCommand("ProjectJerte_GiveAlfie", GiveAlfieCommand);
        }

      private void GiveAlfieCommand(Event @event, string[] args, EventContext context)
        {
            try
            {
                // Instanciamos a Alfie en la baldosa 64, 16
                Pet alfie = new Pet(64, 16, "ProjectJerte.Alfie", "0")
                {
                    Name = "Alfie"
                };

                // Lo inyectamos directamente en la lista de NPCs del mapa de la Granja.
                // El motor de la 1.6 se encarga de registrarlo internamente.
                Game1.getFarm().characters.Add(alfie);

                this.Monitor.Log("Alfie entregado e inyectado en la granja con éxito.", LogLevel.Debug);
            }
            catch (Exception ex)
            {
                this.Monitor.Log($"Error al instanciar a Alfie: {ex.Message}", LogLevel.Error);
            }

            // CRÍTICO: Avanzar el puntero del evento
            @event.CurrentCommand++;
        }
        
    }
}