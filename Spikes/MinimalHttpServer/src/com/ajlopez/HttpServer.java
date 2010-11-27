package com.ajlopez;

import java.io.*;
import java.net.*;

public class HttpServer {

	public static void main(String[] args) {
		int port;
		ServerSocket serversocket;
		
		port = Integer.parseInt(args[0]);
		String rootpath = args[1];
		
		try {
			serversocket = new ServerSocket(port);
		} catch (IOException e) {
			e.printStackTrace();
			return;
		}
		
		while (true) {
			try {
				Socket socket = serversocket.accept();
				BufferedReader reader = new BufferedReader(new InputStreamReader(socket.getInputStream()));
				String line = reader.readLine();
				String [] words = line.split(" ");
				System.out.println(line);
				InputStream stream = new FileInputStream(rootpath + words[1]);
				OutputStream output = socket.getOutputStream();
				byte [] buffer = new byte[4096];
				int nbytes;
				
				while ((nbytes = stream.read(buffer))!=-1)
					output.write(buffer,0,nbytes);
				
				output.close();
				stream.close();
				socket.close();
			} catch (IOException e) {
				e.printStackTrace();
			}
		}
	}

}
