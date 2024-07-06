const REDIS_SOCKET_HOST = process.env.REDIS_SOCKET_HOST || 'redis.socket';
const REDIS_SOCKET_PORT = process.env.REDIS_SOCKET_PORT || 6379;
const REDIS_SOCKET_CONNECTION_STRING = `redis://${REDIS_SOCKET_HOST}:${REDIS_SOCKET_PORT}`;

/**
 * Configure microservice
 */
const SERVER_NAME = process.env.SERVER_NAME || 'Node Socket';
const SERVER_PORT = process.env.SERVER_PORT || 3000;

/**
 * Configure JWT
 */
const JWT_SECRET = process.env.JWT_SECRET || '7kNCc9LwBWbkSctsCq2NEGzzTTNcXCf0FPxDlHJmBcJpRUIYDPn2EgmsdDzXNBnS';
const JWT_ALGO = process.env.JWT_ALGO || 'HS256';

/**
 * Import modules
 */
import { Server } from 'socket.io';
import { createClient } from 'redis';
import { createAdapter } from '@socket.io/redis-adapter';
import jwt from 'jsonwebtoken';
/**
 * Create redis
 */
const pubClient =  createClient({
    url: REDIS_SOCKET_CONNECTION_STRING});
const subClient =  pubClient.duplicate();

pubClient.on('connect', () => {
    console.debug('Pub connection to Redis server ok');
});
subClient.on('connect', () => {
    console.debug('Sub connection to Redis server ok');
});

/**
 * Create socket server
 */
const io = new Server();

/**
 * Процесс авторизации пользователя и создание комнаты
 */
// io.use((socket, next) => {
//
//     const token = socket.handshake.auth.token;
//     console.debug('MiddleWare: ' + token);
//     // Мы можем по JWT ключу получить id пользователя
//
//     try {
//         const decodedToken = jwt.verify(token, JWT_SECRET, { algorithms: [JWT_ALGO] });
//         console.debug('Расшифрованный токен:', decodedToken);
//         const userId = decodedToken.sub; // Получаем Id пользователя
//         socket.join('userId_' + userId); // Присоединяем его в персональную комнату
//
//         // Нужно понять - пользователь админ или нет, и если он админ - добавить его в комнату админов
//         socket.join('admins')
//     } catch (err) {
//         console.error('Ошибка расшифровки токена:', err.message);
//     }
//
//
//     next(); // Идем дальше
// });

Promise.all([pubClient.connect(), subClient.connect()]).then(() => {

    io.adapter(createAdapter(pubClient, subClient));

    io.on('connection', (socket) => {
        socket.emit('socket.myNameIs', SERVER_NAME);
        console.debug('connection: ' + socket.handshake.address);

        socket.on('disconnect', data => {
            console.debug('disconnect: ' + socket.handshake.address);
        })

        socket.on('joinRoom', (roomName) => {
            socket.join(roomName);
            console.debug(`Socket ${socket.id} joined room: ${roomName}`);
        });

        socket.on('leaveRoom', (roomName) => {
            socket.leave(roomName);
            console.debug(`Socket ${socket.id} leaved room: ${roomName}`);
        });
    })

    io.listen(SERVER_PORT);
});

// Пинг сервера - для всех (пустое бессмысленное сообщение)
setInterval(() =>{
    io.emit('ping', Date.now())
}, 30000)
