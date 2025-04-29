using Cysharp.Threading.Tasks;

public interface ISoundService
{
    public UniTask PlaySound(string audioId);
}