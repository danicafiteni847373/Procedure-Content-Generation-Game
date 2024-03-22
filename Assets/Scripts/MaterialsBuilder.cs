using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialsBuilder
{

    private List<Material> materialsList = new List<Material>();

    public MaterialsBuilder()
    {
        Material blackMaterial = new Material(Shader.Find("Specular"));
        blackMaterial.color = Color.black;

        Material grayMaterial = new Material(Shader.Find("Specular"));
        grayMaterial.color = Color.grey;

        Material redMaterial = new Material(Shader.Find("Specular"));
        redMaterial.color = Color.red;

        Material magentaMaterial = new Material(Shader.Find("Specular"));
        magentaMaterial.color = Color.magenta;

        Material yellowMaterial = new Material(Shader.Find("Specular"));
        yellowMaterial.color = Color.yellow;

        Material whiteMaterial = new Material(Shader.Find("Specular"));
        whiteMaterial.color = Color.white;

        Material blueMaterial = new Material(Shader.Find("Specular"));
        blueMaterial.color = Color.blue;

        Material greenMaterial = new Material(Shader.Find("Specular"));
        greenMaterial.color = Color.green;

        Material cyanMaterial = new Material(Shader.Find("Specular"));
        cyanMaterial.color = Color.cyan;


        materialsList.Add(blackMaterial);
        materialsList.Add(grayMaterial);
        materialsList.Add(redMaterial);
        materialsList.Add(magentaMaterial);
        materialsList.Add(yellowMaterial);
        materialsList.Add(whiteMaterial);
        materialsList.Add(blueMaterial);
        materialsList.Add(greenMaterial);
        materialsList.Add(cyanMaterial);
    }

    public List<Material> MaterialsList()
    {
        return materialsList;
    }

}
