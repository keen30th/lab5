using onta_nicusor_lab2.Models;

public class PublishedBook
{
    public int PublisherID { get; set; }
    public int BookID { get; set; }
    public Publisher Publisher { get; set; }
    public Book Book { get; set; }
}