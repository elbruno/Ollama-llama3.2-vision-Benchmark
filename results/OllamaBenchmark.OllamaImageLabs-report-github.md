```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.2605)
Intel Core i9-14900KF, 1 CPU, 32 logical and 24 physical cores
.NET SDK 9.0.200-preview.0.24575.35
  [Host]     : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2 [AttachedDebugger]
  Job-PNKSRD : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2

IterationCount=5  RunStrategy=Throughput  

```
| Method                                   | Mean     | Error     | StdDev    |
|----------------------------------------- |---------:|----------:|----------:|
| Ollama-CPU-llama3.2-vision-Sample-images | 67.095 s | 42.1411 s | 10.9439 s |
| Ollama-CPU-llama3.2-vision-TrafficCams   | 33.026 s |  1.0501 s |  0.1625 s |
| Ollama-GPU-llama3.2-vision-Sample-images |  5.839 s |  8.9504 s |  1.3851 s |
| Ollama-GPU-llama3.2-vision-TrafficCams   |  2.252 s |  0.1875 s |  0.0487 s |
