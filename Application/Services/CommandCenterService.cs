namespace AssetRouter.Application.Services;

using AssetRouter.Core.Entities;
using AssetRouter.Core.Interfaces;
using AssetRouter.Core.Services;

public class CommandState {
    public decimal MonthlySalary { get; set; } = 15000000m;
    public List<AllocationNode> CurrentNodes { get; set; } = new();
    public LiquidOverflowResult OverflowResult { get; set; } = new();
    public StressTestResult ActiveStressTestResult { get; set; } = new();
    public List<TimelineUniverse> ParallelUniverses { get; set; } = new();
    public StressTestScenario SelectedScenario { get; set; } = new();
}

public class CommandCenterService {
    private readonly INodeRepository _nodeRepository;
    private readonly LiquidOverflowEngine _overflowEngine;
    private readonly StressTestEngine _stressTestEngine;
    private readonly TimelineProjectionEngine _timelineEngine;

    public CommandCenterService(INodeRepository nodeRepository) {
        _nodeRepository = nodeRepository;
        _overflowEngine = new LiquidOverflowEngine();
        _stressTestEngine = new StressTestEngine();
        _timelineEngine = new TimelineProjectionEngine();
    }

    public List<StressTestScenario> GetCrisisScenarios() {
        return _stressTestEngine.GetAvailableScenarios();
    }

    public async Task<CommandState> LoadCommandCenterStateAsync(decimal salary) {
        var nodes = await _nodeRepository.GetNodesAsync();
        var scenarios = _stressTestEngine.GetAvailableScenarios();
        var selectedScenario = scenarios.FirstOrDefault() ?? new StressTestScenario();

        return RecalculateAll(salary, nodes, selectedScenario);
    }

    public CommandState RecalculateAll(decimal salary, List<AllocationNode> nodes, StressTestScenario scenario) {
        var overflowResult = _overflowEngine.CalculateAllocationWithOverflow(salary, nodes);
        var stressResult = _stressTestEngine.RunSimulation(scenario, overflowResult.ProcessedNodes, salary);
        var universes = _timelineEngine.GenerateParallelUniverses(salary, overflowResult.ProcessedNodes);

        return new CommandState {
            MonthlySalary = salary,
            CurrentNodes = overflowResult.ProcessedNodes,
            OverflowResult = overflowResult,
            ActiveStressTestResult = stressResult,
            ParallelUniverses = universes,
            SelectedScenario = scenario
        };
    }

    public async Task SaveNodePositionsAsync(List<AllocationNode> nodes) {
        await _nodeRepository.SaveNodesAsync(nodes);
    }
}
