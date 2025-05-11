using System;


public interface ISceneManagerService
{
    public void LoadSceneAsyncAllClients(string sceneName);
    public IProgress<float> GetSceneProgress();

}