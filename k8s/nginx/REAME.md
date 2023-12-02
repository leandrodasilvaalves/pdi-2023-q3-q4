# Load
```sh
kubectl run -i --tty load-generator --rm --image=busybox:1.28 --restart=Never -- /bin/sh -c "while sleep 0.01; do wget -q -O- http://service-name; done"
```

```sh
while sleep 0.01;
do 
  wget -q -O- http://blue.leandroalves.dev.br
  wget -q -O- http://pink.leandroalves.dev.br
done
```
# Reference HPA
- https://kubernetes.io/docs/tasks/run-application/horizontal-pod-autoscale/