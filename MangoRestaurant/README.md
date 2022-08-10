# Set up

---

### Configure docker machine

```shell
podman machine stop
podman machine rm
podman machine init --cpus 8 --memory 8096 --disk-size 50
podman machine start
```
