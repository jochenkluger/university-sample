#wait for the RabbitMq Server to come up
sleep 30s

echo "running set up"
#run the setup script to create the user and vhost
rabbitmqctl add_user dev 123
rabbitmqctl add_vhost uni
rabbitmqctl set_permissions -p uni dev ".*" ".*" ".*"