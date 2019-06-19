using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FilePathHelper : Singleton<FilePathHelper>
{
    //各个平台的StreamAssets路径
    public string StreamingAssetsPath { private set; get; }

    //各个平台的StreamAssets的WWW加载方式的路径
    public string StreamingAssetsPathForWWW { private set; get; }

    //各个平台的PersistentDataPath路径,可读写
    public string PersistentDataPath { private set; get; }

    //各个平台的PersistentDataPath的WWW加载方式的路径,可读写
    public string PersistentDataPathForWWW { private set; get; }

    protected override void Initialize()
    {

#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
        StreamingAssetsPathForWWW = string.Format("file:///{0}/StreamingAssets/", Application.dataPath);
        StreamingAssetsPath = string.Format("{0}/StreamingAssets/", Application.dataPath);
        PersistentDataPathForWWW = string.Format("file:///{0}/StreamingAssets/", Application.dataPath);
        PersistentDataPath = string.Format("{0}/StreamingAssets/", Application.dataPath);
#elif UNITY_ANDROID && !UNITY_EDITOR
        StreamingAssetsPathForWWW = string.Format("jar:file://{0}!/assets/", Application.dataPath);
        StreamingAssetsPath = string.Format("{0}!assets/", Application.dataPath);
        PersistentDataPathForWWW = string.Format("file://{0}/", Application.persistentDataPath);
        PersistentDataPath = string.Concat(Application.persistentDataPath,"/");
#elif UNITY_IOS && !UNITY_EDITOR
        StreamingAssetsPathForWWW = string.Format("file://{0}/Raw/", Application.dataPath);
        StreamingAssetsPath = string.Format("{0}/Raw/", Application.dataPath);
        PersistentDataPathForWWW = string.Format("file://{0}/", Application.persistentDataPath);
        PersistentDataPath = string.Concat(Application.persistentDataPath,"/");
#else
        StreamingAssetsPathForWWW = string.Format("file://{0}/StreamingAssets/", Application.dataPath);
        StreamingAssetsPath = string.Format("{0}/StreamingAssets/", Application.dataPath);
        PersistentDataPathForWWW = string.Format("file://{0}/StreamingAssets/", Application.dataPath);
        PersistentDataPath = string.Format("{0}/StreamingAssets/", Application.dataPath);
#endif
    }


    public string GetStreamingPath(string path)
    {
        return string.Concat(StreamingAssetsPath, path);
    }

    public string GetStreamingPathForWWW(string path)
    {
        return string.Concat(StreamingAssetsPathForWWW, path);
    }

    public string GetPersistentDataPath(string path)
    {
        return string.Concat(PersistentDataPath, path);
    }

    public string GetPersistentDataPathForWWW(string path)
    {
        return string.Concat(PersistentDataPathForWWW, path);
    }
}
