Neural network inverted spring pendulum
=====================
## Condition
The segment of the values ​​of the coefficient of elasticity k = [0.1; 0.2 + (# div 3) / 10].

The segment of the values ​​of the initial velocity of the weight v = [0.1; 1 - [# div 6) / 10]

Number of observation points P = 5 + (# mod 3).

Observation points - x_1, x_2, ... x_i ..., x_P.
x_i = x (t0 + (i-1) * dt).
For dt = # / 1000.
t0 = [0; T], T is the period of oscillations.

The values ​​of physical quantities are given in the basic units of the SI system.

Assume that
div - integer division (rounding down),
mod - the remainder of the integer division,
\# - option number.

The size of the input of the neural network - P (observation point).
The size of the output of the neural network - 1 (coefficient of elasticity).
The number of sampling elements - N = 125000 = 50 different values ​​of k * 50 different values ​​of v * 50 different values ​​of t0 (values ​​are selected evenly on given segments).

## Environment
- R version 4.0.3 (Bunny-Wunnies Freak Out)
- C# 9.0 with .NET 5.0

## Components
- DataGenerator using for generate data with params by neural network
- data.txt is generated file with data
- NeuralNetworkInvertedPendulum solution is neural network that using data.txt
- Neural.workspace.RData is exported memory workspace from R with trained neural network

## About:
Neural network inverted spring pendulum developing under the [MIT license](LICENSE).
