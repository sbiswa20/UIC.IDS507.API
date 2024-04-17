

namespace RRHTSCLASSIFIERAPI.Models;

public class Node
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public List<Node> Children { get; set; } = new List<Node>();

        public Node(string id, string description)
        {
            Id = id;
            Description = description;
        }

        public void AddChild(Node child)
        {
            //Children.Add(child);

            if (child.Children.Count > 0 || !String.IsNullOrWhiteSpace(child.Description)) // Optionally check for other conditions
            {
                Children.Add(child);
            }
        }
    }