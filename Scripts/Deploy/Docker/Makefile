build :
	./build.sh

debug :
	docker run -it --entrypoint=/bin/bash --privileged --net=birdy_net --ip=10.0.1.3 -p 127.0.0.1:8888:80 -v /mnt/j/DSLRPhotos:/mnt/DSLRPhotos -v /mnt/k/DSLR2:/mnt/DSLR2 -v $(shell pwd)/override/appsettings.json:/Birdy/appsettings.json -v $(shell pwd)/override/entrypoint.sh:/Birdy/entrypoint.sh --name=birdy_c birdy


network :
	docker network create --subnet=10.0.1.0/24 birdy_net

run_mongo :
	docker run -d --net=birdy_net --ip=10.0.1.2 --name=mongo_c mongo

run_birdy :
	docker run -d --net=birdy_net --ip=10.0.1.3 -p 127.0.0.1:8888:80 -v /mnt/j/DSLRPhotos:/mnt/DSLRPhotos -v /mnt/k/DSLR2:/mnt/DSLR2 -v $(shell pwd)/override/appsettings.json:/Birdy/appsettings.json -v $(shell pwd)/override/entrypoint.sh:/Birdy/entrypoint.sh --name=birdy_c birdy

clean_birdy :
	-docker kill birdy_c ; docker rm birdy_c

clean_mongo :
	-docker kill mongo_c ; docker rm mongo_c

clean_net :
	-docker network rm birdy_net

clean : clean_birdy clean_mongo clean_net

run : network run_mongo run_birdy
