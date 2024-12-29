# Image Analysis Benchmark: Llama 3.2 Vision

This repository demonstrates benchmarking the **Llama 3.2 Vision** model using **Ollama** for image analysis tasks. The tests compare the performance of running the model locally on **CPU** vs **GPU** using Docker.

## Review Video

For a detailed review of this repository, [watch this video](https://youtu.be/3JpoISL_Fx0).

## Test Scenarios

Two image analysis scenarios were benchmarked:

1. **Camping Products Image**: Analyze a product image and generate a JSON description in English, French, and Spanish.
2. **CCTV Traffic Camera Image**: Analyze a traffic camera frame and generate a JSON object containing:
   - Camera title
   - Photo date and time
   - Traffic level (0 to 100)

## Hardware Used

- **CPU**: Intel(R) Core(TM) i9-14900KF
- **GPU**: NVIDIA GeForce RTX 4070 Ti SUPER

## Benchmark Results

### **Scenario 1: Camping Products Image Analysis**

| Metric              | CPU (Time/Op)   | GPU (Time/Op)   | GPU Speed-Up |
|---------------------|-----------------|----------------|--------------|
| Average             | 50.4 - 70+ sec | 5.3 - 6.8 sec  | ~10x faster  |

### **Scenario 2: CCTV Traffic Camera Analysis**

| Metric              | CPU (Time/Op)   | GPU (Time/Op)   | GPU Speed-Up |
|---------------------|-----------------|----------------|--------------|
| Average             | 1.1 - 1.28 min | 32 - 38 sec    | ~2-3x faster |

## Observations

1. **GPU Efficiency**:
   - GPUs, particularly the RTX 4070 Ti SUPER, significantly outperform CPUs for image analysis tasks, achieving up to 10x faster processing in simple scenarios like product image descriptions.
   - In more complex tasks like traffic analysis, GPUs still provide 2-3x speed-ups, though the benefit narrows due to increased workload complexity.

2. **CPU Bottlenecks**:
   - Modern CPUs such as the Intel i9-14900KF are limited in their parallel processing capabilities, making them less suitable for high-throughput tasks like AI model inference.

3. **Scalability**:
   - The acceleration depends heavily on the specific GPU hardware and the parallelizable nature of the workload.

## Software and Tools

- **Ollama**: Used to run the Llama 3.2 Vision model locally in Docker.
- **BenchmarkDotNet**: .NET library utilized to conduct benchmarks.

## How to Reproduce

1. Install **Docker**.

1. Run the Docker container for GPU and CPU:

   - GPU running on port 11434 (default)

   ```bash
   docker run -d --gpus=all -v ollama:/root/.ollama -p 11434:11434 --name ollama ollama/ollama
   ```

   - CPU running on port 11435

    ```bash
    docker run -d -v ollamacpu:/root/.ollamacpu -p 11435:11434 --name ollamacpu ollama/ollama
    ```

1. On each container pull the llama3.2-vision image. Run the command

   ```bash
   ollama run llama3.2-vision
   ```

1. Clone this repository and run the benchmarking project:

   ```bash
   git clone <repository-url>
   cd <repository-directory>
   dotnet run --configuration Release
   ```

## Recommendations

- **Production Environments**: Use a high-performance GPU to minimize latency in real-time AI applications.
- **Development Environments**: CPUs may suffice for prototyping but are not ideal for deployment due to higher processing times.
- **Task Complexity**: For highly parallelizable tasks, GPUs provide significant benefits. Consider GPU selection based on model and task requirements.

## License

This project is licensed under the MIT License. See the LICENSE file for details.