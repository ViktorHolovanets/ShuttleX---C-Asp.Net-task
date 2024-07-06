
/**
 * Configure RabbitMQ server
 */
const RABBITMQ_DEFAULT_USER = process.env.RABBITMQ_DEFAULT_USER || 'user';
const RABBITMQ_DEFAULT_PASS = process.env.RABBITMQ_DEFAULT_PASS || 'password';
const RABBITMQ_SERVER = process.env.RABBITMQ_SERVER || 'rabbitmq';
const RABBITMQ_PORT = process.env.RABBITMQ_PORT || 5672;
const RABBITMQ_CONNECTION_URI = `amqp://${RABBITMQ_DEFAULT_USER}:${RABBITMQ_DEFAULT_PASS}@${RABBITMQ_SERVER}:${RABBITMQ_PORT}`;

const RABBITMQ_QUEUE_SOCKET = process.env.RABBITMQ_QUEUE_SEND_EMAIL || 'queue_emitter';


/**
 * Configure Redis server
 */
const REDIS_SOCKET_HOST = process.env.REDIS_SOCKET_HOST || 'redis.socket';
const REDIS_SOCKET_PORT = process.env.REDIS_SOCKET_PORT || 6379;
const REDIS_SOCKET_CONNECTION_STRING = `redis://${REDIS_SOCKET_HOST}:${REDIS_SOCKET_PORT}`;


import rabbitMQHandler from "typescript-rabbitmq-handler";
import IEvent from "./models/IEvent";
import {Emitter} from "@socket.io/redis-emitter";
import {createClient} from "redis";

(async () => {
    try {
        const redisClient = createClient({url: REDIS_SOCKET_CONNECTION_STRING});
        await redisClient.connect();

        const io = new Emitter(redisClient);

        try {
            const rabbit = await rabbitMQHandler.create(RABBITMQ_CONNECTION_URI);
            await rabbit.startListening<IEvent>(RABBITMQ_QUEUE_SOCKET, async (msg: IEvent) => {
                if (msg.To) {
                    console.log(`emit for ${msg.To}`)
                    io.to('userId_' + msg.To).emit(msg.Template, msg);
                } else {
                    console.log(`emit for everybody`)
                    io.emit(msg.Template, msg);
                }
            });
        } catch (err) {
            console.error(err);
        }
    } catch (e) {
        console.error("Error connecting to Redis:", e);
    }
})();