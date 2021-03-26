#require(readxl)
require(nnet)
require(ggplot2)
require(functional)

#obj <- as.data.frame(read_xlsx("dataset-example.xlsx", sheet = 2))
obj <- as.data.frame(read.table("data.txt", header = TRUE, sep = ","))
# ----------------------------------------------------------------------------------------
# ------------------------------------ Learning stage ------------------------------------
# ----------------------------------------------------------------------------------------
obj$k_norm = factor(obj$k_norm) # for nnet
write.csv(obj, "out.csv")


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

# Function of activation: L/1 + e^(-k*(x-x0)) - logistic function
nn <- nnet(k_norm ~ x0_norm + x1_norm + x2_norm + x3_norm + x4_norm + x5_norm, data = obj,
           subset = sampidx, size = 10, decay = 1.0e-5, maxit = 50)


# ---------------------------------------------------------------------------------------
# ------- Testing stage (need reread dataset without factor usage instead k_norm) -------
# ---------------------------------------------------------------------------------------
View(as.data.frame(table(obj$k_norm[-sampidx], predict(nn, obj[-sampidx, ], type = "class"))))

#prediction <- as.data.frame(table(obj$k_norm[-sampidx], predict(nn, obj[-sampidx, ], type = "class")))
#prediction <- prediction[apply(prediction, 1, Compose(
#  function(item) {
#    return(item[1] == item[2])
#  }, all)),]

# ok - 41, all - 1250, percentage - 0.03%

mean((predict(nn, obj[-sampidx, ]) - obj[-sampidx, ]$k_norm)^2)


#0.6530308 	0.2946015 	0.1250147 	0.3001876 	0.6590671 	1.0000000 
#x <- data.frame(0.6530308, 0.2946015, 0.1250147, 0.3001876, 0.6590671, NA)
#names(x) <- c("x0_norm", "x1_norm", "x2_norm", "x3_norm", "x4_norm", "k_norm")
#predict(nn, x, type = "class")

#0.5000000 	0.5000000 	0.5000000 	0.5000000 	0.5000000 	0.0000000 
#x1 <- data.frame(0.5000000, 0.5000000, 0.5000000, 0.5000000, 0.5000000, NA)
#names(x1) <- c("x0_norm", "x1_norm", "x2_norm", "x3_norm", "x4_norm", "k_norm")
#predict(nn, x1, type = "class")
