#region

using MagicOnion;

#endregion

namespace ASPDotnetLearn.Shared
{
    public interface IMyFirstService : IService<IMyFirstService>
    {
        UnaryResult<int> SumAsync(int x, int y);
    }
}