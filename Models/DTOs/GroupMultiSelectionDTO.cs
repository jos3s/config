namespace config.Models.DTOs;
internal class GroupMultiSelectionDTO
{
    public string Name { get; set; }

    public List<string> Options { get; set; } = new List<string>();
}
