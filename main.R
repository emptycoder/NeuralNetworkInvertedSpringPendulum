#require(readxl)
require(nnet)

#obj <- as.data.frame(read_xlsx("dataset-example.xlsx", sheet = 2))
obj <- as.data.frame(read.table("data.txt", header = TRUE, sep = ","))
obj$k_norm = factor(obj$k_norm)

set.seed(1)
# Every sample contains 12 500 elements
sampidx <- c(sample(1:12500,12375),
             sample(12501:25000,12375),
             sample(25001:37500,12375),
             sample(37501:50000,12375),
             sample(50001:62500,12375),
             sample(62501:75000,12375),
             sample(75001:87500,12375),
             sample(87501:100000,12375),
             sample(100001:112500,12375),
             sample(112501:125000,12375))

nn <- nnet(k_norm ~ x0_norm + x1_norm + x2_norm + x3_norm + x4_norm + x5_norm + x6_norm, data = obj,
           subset = sampidx, size = 10, decay = 1.0e-5, maxit = 50)

table(obj$k_norm[-sampidx], predict(nn, obj[-sampidx, ], type = "class"))

#0.6530308 	0.2946015 	0.1250147 	0.3001876 	0.6590671 	1.0000000 
#x <- data.frame(0.6530308, 0.2946015, 0.1250147, 0.3001876, 0.6590671, NA)
#names(x) <- c("x0_norm", "x1_norm", "x2_norm", "x3_norm", "x4_norm", "k_norm")
#predict(nn, x, type = "class")

#0.5000000 	0.5000000 	0.5000000 	0.5000000 	0.5000000 	0.0000000 
#x1 <- data.frame(0.5000000, 0.5000000, 0.5000000, 0.5000000, 0.5000000, NA)
#names(x1) <- c("x0_norm", "x1_norm", "x2_norm", "x3_norm", "x4_norm", "k_norm")
#predict(nn, x1, type = "class")
