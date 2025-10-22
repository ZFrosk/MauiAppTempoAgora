namespace MauiAppTempoAgora.Models
{
    public class Tempo
    {
        // Descrição do Tempo
        public string? Description { get; set; }

        // Condição Principal do Tempo
        public string? Main { get; set; }

        // Longitude
        public double? Lon { get; set; }

        // Latitude
        public double? Lat { get; set; }

        // Visibilidade
        public long? Visibility { get; set; }

        // Temperatura Mínima
        public double? TempMin { get; set; }

        // Temperatura Máxima
        public double? TempMax { get; set; }

        // Nascer do Sol
        public string? Sunrise { get; set; }

        // Pôr do Sol
        public string? Sunset { get; set; }

        // Velocidade do Vento
        public double? Speed { get; set; }

    }
}
