using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[ExecuteAlways]
public class UnityMainThread : MonoBehaviour
{
	public static UnityMainThread InstanceUnityMainThread
	{
		get
		{
			if (instance == null)
				throw new Exception("Проинициализируй UnityMainThread");
			else return instance;
		}
	}
	private static UnityMainThread instance;
	private readonly Queue<Task> _jobs = new Queue<Task>();

	private void Awake() {
		if (instance == null) instance = this;   
		else if (instance == this) Destroy(gameObject);
		DontDestroyOnLoad(this);
	}

	private void Update() {
		while (_jobs.Count > 0)
		{
			var job = _jobs.Dequeue();
			job.RunSynchronously();
			print("Update Main Thread queued job");
		}
	}

	public Task AddJob(Task newJob) {
		_jobs.Enqueue(newJob);
		return newJob;
	}
}