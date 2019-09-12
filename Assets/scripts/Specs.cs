using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Specs
{
    public float width;
    public float height;
    public float depth;
    public string colour;

    public Specs(float width, float height, float depth, string colour)
    {
        this.width = width;
        this.height = height;
        this.depth = depth;
        this.colour = colour;
    }

    public string GetSpecs()
    {
        string specs = "Width: " + width + "\n"
            + "Height: " + height + "\n"
            + "Depth: " + depth + "\n"
            + "Colour: " + colour + "\n";

        return specs;
    }
}

