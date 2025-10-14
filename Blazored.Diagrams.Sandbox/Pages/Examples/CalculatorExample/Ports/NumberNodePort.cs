using Blazored.Diagrams.Interfaces;
using Blazored.Diagrams.Ports;
using Blazored.Diagrams.Sandbox.Pages.Examples.CalculatorExample.Nodes;
using Blazored.Diagrams.Sandbox.Pages.Examples.CalculatorExample.OperatorNode;

namespace Blazored.Diagrams.Sandbox.Pages.Examples.CalculatorExample.Ports;

public class NumberNodePort : Port
{
    public new required NumberNode Parent { get; set; }
    public Nodes.OperatorNode? Target => OutgoingLinks.FirstOrDefault()?.TargetPort?.Parent as Nodes.OperatorNode;
    public override bool CanCreateLink()
    {
        return OutgoingLinks.Count == 0;
    }

    public override bool CanConnectTo(IPort port)
    {
        return port is OperatorInputPort;
    }
}